import { Component, OnInit } from '@angular/core';
import { ModalComponent } from '../../ui/modal/modal.component';
import { ReservationFormComponent } from '../reservation-form/reservation-form.component';
import { ToastrService } from 'ngx-toastr';
import { ReservationService } from '../../../services/reservation.service';
import { IReservation } from '../../shared/models/Reservation';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [ModalComponent, ReservationFormComponent, DatePipe],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss',
})
export class ReservationComponent implements OnInit {
  isModalOpen = false;
  reservations: IReservation[] = [];
  reservation!: IReservation;

  constructor(
    private reservationService: ReservationService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getAllReservations();
  }

  getAllReservations() {
    this.reservationService.getAllReservations().subscribe({
      next: (response) => {
        if (response.data) {
          this.reservations = response.data;
        }
      }
    });
  }

  loadReservation(reservation: IReservation) {
    console.log(reservation)
    this.reservation = reservation;
    this.openModal();
  }

  deleteReservation(id: number) {
    this.reservationService.deleteReservation(id).subscribe({
      next: (response) => {
        this.toastr.success(response.message);
        this.getAllReservations();
      },
    });
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.getAllReservations();
  }
}
