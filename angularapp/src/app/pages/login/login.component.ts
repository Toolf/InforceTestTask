import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { TokenStorageService } from 'src/app/services/token-storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  user = {
    username: '',
    password: ''
  };


  constructor(private authService: AuthService, private tokenStorage: TokenStorageService, private router: Router) { }

  onSubmit() {
    this.authService.login(this.user.username, this.user.password).subscribe({
      next: (data) => {
        this.tokenStorage.saveToken((data as JwtData).token);
        this.authService.me().subscribe({
          next: (user) => {
            this.tokenStorage.saveUser(user);
            this.router.navigate(['/short-urls']);
          }
        });
      },
      error: (e) => {
        console.error(e);
      }
    });
  }
}

interface JwtData {
  token: string;
}