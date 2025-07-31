import { Component, OnInit } from '@angular/core';
import { Auth } from '../../core/services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [FormsModule, NgIf],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login implements OnInit {
  username = '';
  password = '';
  error = '';

  constructor(private auth: Auth, private router: Router) {}
  
  ngOnInit(): void {
    console.log("Login started!!!")
  }

  onSubmit() {
    this.error = '';
    this.auth.login({ username: this.username, password: this.password })
      .subscribe({
        next: (res: any) => {
          this.auth.setToken(res.token);
          this.router.navigate(['/dashboard']);
        },
        error: () => {
          this.error = 'Invalid credentials';
        }
      });
  }
}
