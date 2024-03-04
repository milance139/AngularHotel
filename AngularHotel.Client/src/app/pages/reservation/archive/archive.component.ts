import { Component, OnInit, ViewChild } from '@angular/core';
import { ReservationFormComponent } from '../reservation-form/reservation-form.component';
import { ToastrService } from 'ngx-toastr';
import { ReservationService } from '../../../services/reservation.service';
import { IArchivedReservation } from '../../shared/models/response/archivedReservation.model';
import { DatePipe, CommonModule } from '@angular/common';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-archived-reservations',
  standalone: true,
  imports: [ReservationFormComponent, DatePipe, CommonModule, MatPaginatorModule, MatSortModule, MatTableModule, MatFormFieldModule, MatInputModule],
  templateUrl: './archive.component.html',
  styleUrl: './archive.component.scss',
})
export class ArchivedReservationsComponent implements OnInit {
  isModalOpen = false;
  archivedReservations: IArchivedReservation[] = [];

  displayedColumns = ['from', 'to', 'committee', 'original_price', 'discount', 'total_price', 'currency.name', 'total_price_bam', 'total_price_eur', 'reserved_rooms'];
  dataSource: MatTableDataSource<IArchivedReservation> = new MatTableDataSource();

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    private reservationService: ReservationService,
    private toastr: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.getAllArchivedReservations();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  getAllArchivedReservations() {
    this.reservationService.getArchivedReservations().subscribe({
      next: (response) => {
        if (response.data) {
          this.archivedReservations = response.data;
          this.dataSource.data = response.data;
        }
      }
    });
  }

  getTotalPriceEurSum(): number {
    return this.archivedReservations.reduce((acc, item) => acc + item.totalPriceInBAMAndEur.totalPriceEur, 0);
  }

  getTotalPriceBamSum(): number {
    return this.archivedReservations.reduce((acc, item) => acc + item.totalPriceInBAMAndEur.totalPriceBam, 0);
  }
}
