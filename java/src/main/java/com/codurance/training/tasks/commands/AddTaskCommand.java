package com.codurance.training.tasks.commands;

import com.codurance.training.tasks.*;
import com.codurance.training.tasks.input.CommandLine;

import static com.codurance.training.tasks.input.MainCommand.add;
import static com.codurance.training.tasks.input.SubCommand.task;

public class AddTaskCommand implements Command {
    private Projects projects;
    private final IdGenerator idGenerator;

    public AddTaskCommand(Projects projects) {
        this.projects = projects;
        this.idGenerator = new AddTaskCommand.IdGenerator();
    }

    @Override
    public boolean canHandle(CommandLine cmdLine) {
        return cmdLine.is(add, task);
    }

    @Override
    public void handle(CommandLine cmdLine) {
        ProjectId projectId = cmdLine.getProjectId();
        TaskId taskId = new TaskId(idGenerator.nextId());
        Task task = cmdLine.getTask(taskId);
        projects.addTaskWithId(projectId, task);
    }

    public static class IdGenerator {
        private long lastId = 0;

        public IdGenerator() {
        }

        public long nextId() {
            return ++lastId;
        }
    }
}
