import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { ApiResponse } from '../shared/models/ApiResponse';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
      this.loginForm = this.fb.group({
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  }

  onLogin() {
    if (this.loginForm.valid) {
      this.authService.login(this.loginForm.value).subscribe({
        next: (response: ApiResponse<string>) => {
          if (response.success) {
            this.router.navigateByUrl('/reservation');
            this.toastr.success(response.message);
          }
        },
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}

export class Login {
  Email: string;
  Password: string;
  constructor() {
    this.Email = '';
    this.Password = '';
  }
}
