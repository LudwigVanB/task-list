package com.codurance.training.tasks;

import com.codurance.training.tasks.commands.ProjectId;

public class CommandLine {


    private String commandLine;

    public CommandLine(String commandLine) {
        this.commandLine = commandLine;
    }

    public ProjectId getProjectId() {
        return new ProjectId(splitInParts(4)[2]);
    }

    public Task getTask(TaskId taskId) {
        String description = splitInParts(4)[3];
        return new Task(taskId, description);
    }

    public boolean is(MainCommand mainCommand, SubCommand subCommand) {
        return commandLine.matches("^" + mainCommand + " " + subCommand + " .*");
    }


    public Task getTask() {
        String[] parts = splitInParts(5);
        String description = parts[4];
        TaskId taskId = new TaskId(parts[3]);
        return new Task(taskId, description);

    }

    private String[] splitInParts(int nbParts) {
        return commandLine.split(" ", nbParts);
    }
}
