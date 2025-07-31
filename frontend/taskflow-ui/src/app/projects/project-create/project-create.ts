import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { projectService } from '../services/projectService';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-create',
  imports: [],
  templateUrl: './project-create.html',
  styleUrl: './project-create.css'
})
export class ProjectCreate implements OnInit {
  form!: FormGroup;
  
  constructor(
    private fb: FormBuilder,
    private svc: projectService,
    private router: Router
  ) {}

  ngOnInit() {
    // Now fb is initialized by DI
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  submit() {
    if (this.form.invalid) return;
    this.svc.createProject(this.form.value).subscribe(() => {
      this.router.navigate(['/projects']);
    });
  }

  cancel() {
    this.router.navigate(['/projects']);
  }
}
