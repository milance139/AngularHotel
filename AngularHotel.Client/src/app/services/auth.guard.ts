import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const localData = localStorage.getItem('token');

    if (localData != null) {
      return true;
    } else {
      this.router.navigateByUrl('/login');
      return false;
    }
  }
}
