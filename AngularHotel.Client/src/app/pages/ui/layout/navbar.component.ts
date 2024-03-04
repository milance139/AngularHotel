import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../../../services/auth.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent {
  constructor(
    private router: Router,
    public authService: AuthService
  ) {

  }

  logout() {
    this.authService.logout()
    this.router.navigateByUrl('/login');
  }
}
