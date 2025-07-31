import { Component } from '@angular/core';
import { Project, projectService, } from '../services/projectService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-list',
  standalone:true,
  imports: [],
  templateUrl: './project-list.html',
  styleUrl: './project-list.css'
})
export class ProjectList {
  projects: Project[] = [];

  constructor(private svc: projectService, private router: Router) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.svc.getProjects().subscribe((list: Project[]) => this.projects = list);
  }
}
