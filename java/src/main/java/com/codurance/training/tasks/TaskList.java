package com.codurance.training.tasks;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.util.*;
import java.util.stream.Stream;

public final class TaskList implements Runnable {
    private static final String QUIT = "quit";

    private final Map<String, List<Task>> tasks = new LinkedHashMap<>();
    private final BufferedReader in;
    private final PrintWriter out;

    private long lastId = 0;

    public static void main(String[] args) throws Exception {
        BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
        PrintWriter out = new PrintWriter(System.out);
        new TaskList(in, out).run();
    }

    public TaskList(BufferedReader reader, PrintWriter writer) {
        this.in = reader;
        this.out = writer;
    }

    public void run() {
        while (true) {
            out.print("> ");
            out.flush();
            String command;
            try {
                command = in.readLine();
            } catch (IOException e) {
                throw new RuntimeException(e);
            }
            if (command.equals(QUIT)) {
                break;
            }
            execute(command);
        }
    }

    private void execute(String commandLine) {
        String[] commandRest = commandLine.split(" ",2);
        String command = commandRest[0];
        switch (command) {
            case "show":
                show();
                break;
            case "add":
                add(commandRest[1]);
                break;
            case "check":
                check(new TaskId(commandRest[1]));
                break;
            case "uncheck":
                uncheck(new TaskId(commandRest[1]));
                break;
            case "help":
                help();
                break;
            case "deadline":
                commandRest = commandLine.split(" ",3);
                MyDate deadline = new MyDate(commandRest[2]);
                TaskId id = new TaskId(commandRest[1]);
                deadline(new TaskDeadline(id, deadline));
                break;
            case "today":
                today();
                break;
            default:
                error(command);
                break;
        }
    }

    private void today() {
        MyDate today = new MyDate("21/09/2017");
        getAllTasks().filter(task -> task.isDue(today))
                .forEach(task -> task.printTask(out));
    }

    private void deadline(TaskDeadline taskDeadline) {
        Task task = findTask(taskDeadline.id);
        task.setDeadline(taskDeadline.deadline());
    }

    private void show() {
        for (Map.Entry<String, List<Task>> project : tasks.entrySet()) {
            out.println(project.getKey());
            for (Task task : project.getValue()) {
                task.printTask(out);
            }
            out.println();
        }
    }

    private void add(String commandLine) {
        String[] subcommandRest = commandLine.split(" ", 2);
        String subcommand = subcommandRest[0];
        if (subcommand.equals("project")) {
            addProject(subcommandRest[1]);
        } else if (subcommand.equals("task")) {
            String[] projectTask = subcommandRest[1].split(" ", 2);
            addTask(projectTask[0], projectTask[1]);
        } else if (subcommand.equals("task-with-id")) {
            String[] projectTask = subcommandRest[1].split(" ", 3);
            addTaskWithId(projectTask[0], projectTask[1], projectTask[2]);
        }
    }

    private void addProject(String name) {
        tasks.put(name, new ArrayList<Task>());
    }

    private void addTask(String project, String description) {
        List<Task> projectTasks = tasks.get(project);
        if (projectTasks == null) {
            out.printf("Could not find a project with the name \"%s\".", project);
            out.println();
            return;
        }
        long generatedId = nextId();
        TaskId id = new TaskId(generatedId);
        projectTasks.add(new Task(id, description, false));
    }
    private void addTaskWithId(String project, String id, String description) {
        List<Task> projectTasks = tasks.get(project);
        if (projectTasks == null) {
            out.printf("Could not find a project with the name \"%s\".", project);
            out.println();
            return;
        }
        System.out.println("task id " + id);
        TaskId taskId = new TaskId(id);
        projectTasks.add(new Task(taskId, description, false));
    }

    private void check(TaskId taskId) {
        setDone(taskId, true);
    }

    private void uncheck(TaskId taskId) {
        setDone(taskId, false);
    }

    private void setDone(TaskId taskId, boolean done) {
        Task tsk = findTask(taskId);
        if (tsk == null) {
            out.printf("Could not find a task with an ID of %s.", taskId);
            out.println();
        } else {
            tsk.setDone(done);
        }
    }

    private Task findTask(TaskId id) {
        return getAllTasks()
                .filter(task -> task.getId().equals(id))
                .findFirst()
                .orElse(null);
    }

    private Stream<Task> getAllTasks() {
        return tasks.entrySet().stream().flatMap((project -> project.getValue().stream()));
    }

    private void help() {
        out.println("Commands:");
        out.println("  show");
        out.println("  add project <project name>");
        out.println("  add task <project name> <task description>");
        out.println("  check <task ID>");
        out.println("  uncheck <task ID>");
        out.println();
    }

    private void error(String command) {
        out.printf("I don't know what the command \"%s\" is.", command);
        out.println();
    }

    private long nextId() {
        return ++lastId;
    }
}
