import { Component } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-registry',
  templateUrl: './registry.component.html',
  styleUrls: ['./registry.component.css']
})
export class RegistryComponent {
  user = {
    username: '',
    password: ''
  };

  private authService: AuthService;

  constructor(authService: AuthService) {
    this.authService = authService;
  }

  onSubmit() {
    this.authService.registry(this.user.username, this.user.password).subscribe({
      next: (res) => {
        console.log(res);
      },
      error: (e) => {
        console.error(e);
      }
    });
  }
}
