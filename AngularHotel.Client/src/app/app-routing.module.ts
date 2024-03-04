import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoomComponent } from '../app/pages/room/room/room.component'
import { CurrencyComponent } from '../app/pages/currency/currency/currency.component'
import { ReservationComponent } from '../app/pages/reservation/reservation/reservation.component'
import { ArchivedReservationsComponent } from '../app/pages/reservation/archive/archive.component'
import { LoginComponent } from '../app/pages/login/login.component'
import { DashboardComponent } from '../app/pages/dashboard/dashboard.component'
import { ContactComponent } from '../app/pages/contact/contact.component'
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  
  { path: "", component: DashboardComponent },
  { path: "login", component: LoginComponent},
  { path: "contact", component: ContactComponent },
  { path: "room", component: RoomComponent, canActivate: [AuthGuard] },
  { path: "currency", component: CurrencyComponent, canActivate: [AuthGuard] },
  { path: "reservation", component: ReservationComponent, canActivate: [AuthGuard] },
  { path: "archive", component: ArchivedReservationsComponent, canActivate: [AuthGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

