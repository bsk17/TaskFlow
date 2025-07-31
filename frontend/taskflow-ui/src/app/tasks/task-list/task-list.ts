import { Component, OnInit } from '@angular/core';
import { Task } from '../services/task';

@Component({
  selector: 'app-task-list',
  imports: [],
  templateUrl: './task-list.html',
  styleUrl: './task-list.css'
})
export class TaskList implements OnInit {

  tasks: any[] = [];
  projectId = 1; // Example: set programmatically or by route param

  constructor(private taskService: Task) {}

  ngOnInit(): void {
    this.taskService.getTasksByProject(this.projectId)
      .subscribe(tasks => this.tasks = tasks);
  }

}
