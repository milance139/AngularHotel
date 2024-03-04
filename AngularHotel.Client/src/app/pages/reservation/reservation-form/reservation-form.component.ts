import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
} from '@angular/core';
import { IReservation } from '../../shared/models/Reservation';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReservationService } from '../../../services/reservation.service';
import { RoomService } from '../../../services/room.service';
import { CurrencyService } from '../../../services/currency.service';
import { CommitteeService } from '../../../services/comitee.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-reservation-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './reservation-form.component.html',
  styleUrl: './reservation-form.component.scss',
})
export class ReservationFormComponent implements OnChanges {
  @Input() data: IReservation | null = null;
  @Output() onCloseModal = new EventEmitter();

  reservationForm!: FormGroup;
  reservedRooms: { id: number, name: string }[] = [];
  currencies: { id: number, name: string }[] = [];
  reservationCommitteeMembers: { id: number, fullName: string }[] = [];
 
  constructor(
    private fb: FormBuilder,
    private reservationService: ReservationService,
    private roomService: RoomService,
    private currencyService: CurrencyService,
    private committeeService: CommitteeService,
    private toastr: ToastrService
  ) {
    this.reservationForm = this.fb.group({
      from: ['', Validators.required],
      to: ['', Validators.required],
      reservationCommitteeId: ['', Validators.required],
      originalPrice: ['', Validators.required],
      discount: [null, [Validators.min(0), Validators.max(100)]],
      currencyId: ['', Validators.required],
      totalPrice: [{ value: '', disabled: true }],
      reservedRoomIds: [[]],
    });

    this.getAllRooms();
    this.getAllCurrencies();
    this.getAllCommittees();
  }

  ngOnInit(): void {
    this.reservationForm.get('originalPrice')?.valueChanges.subscribe(() => {
      this.calculateTotalPrice();
    });

    this.reservationForm.get('discount')?.valueChanges.subscribe(() => {
      this.calculateTotalPrice();
    });
  }

  onClose() {
    this.onCloseModal.emit(false);
  }

  ngOnChanges(): void {
    if (this.data) {
      this.reservationForm.patchValue({
        from: this.data.from,
        to: this.data.to,
        reservationCommitteeId: this.data.reservationCommitteeId,
        originalPrice: this.data.originalPrice,
        discount: this.data.discount,
        totalPrice: this.data.totalPrice,
        currencyId: this.data.currency.id,
        reservedRoomIds: this.data.reservedRooms.map(room => room.id)
      });
    }
  }

  getAllRooms() {
    this.roomService.getAllRooms().subscribe({
      next: (response) => {
        if (response.data) {
          this.reservedRooms = response.data
            .filter(room => !room.isDeleted)
            .map(room => ({ id: room.id!, name: room.name! }));
        }
      }
    });
  }

  getAllCurrencies() {
    this.currencyService.getAllCurrencies().subscribe({
      next: (response) => {
        if (response.data) {
          this.currencies = response.data
            .map(currenciy => ({ id: currenciy.id!, name: currenciy.name! + " - " + currenciy.price }));;
        }
      }
    });
  }

  getAllCommittees() {
    this.committeeService.getAllCommittees().subscribe({
      next: (response) => {
        if (response.data) {
          this.reservationCommitteeMembers = response.data
            .map(committee => ({ id: committee.id!, fullName: committee.firstName! + " " + committee.lastName }));;
        }
      }
    });
  }

  calculateTotalPrice(): void {
    const originalPrice = this.reservationForm.get('originalPrice')?.value;
    const discount = this.reservationForm.get('discount')?.value;
    if (!discount) {
      this.reservationForm.get('totalPrice')?.setValue(originalPrice.toFixed(2));
    }
    else if (originalPrice !== null && discount !== null) {
      const discountedPrice = originalPrice * (1 - discount / 100);
      this.reservationForm.get('totalPrice')?.setValue(discountedPrice.toFixed(2));
    }
  }

  onSubmit() {
    if (this.reservationForm.valid) {

      const reservedRoomIds = Array.isArray(this.reservationForm.get('reservedRoomIds')?.value) ?
        this.reservationForm.get('reservedRoomIds')?.value :
        [+this.reservationForm.get('reservedRoomIds')?.value];

      const formData = { ...this.reservationForm.value, reservedRoomIds: reservedRoomIds };


      if (this.data) {
        this.reservationService
          .updateReservation(formData)
          .subscribe({
            next: (response: any) => {
              this.resetReservationForm();
              this.toastr.success(response.message);
            },
            error: (error: any) => {
              this.toastr.error(error.message);
            }
          });
      } else {
        this.reservationService.createReservation(formData).subscribe({
          next: (response: any) => {
            this.resetReservationForm();
            this.toastr.success(response.message);
          },
          error: (error: any) => {
            this.toastr.error(error.message);
          }
        });
      }
    } else {
      this.reservationForm.markAllAsTouched();
    }
  }

  resetReservationForm() {
    this.reservationForm.reset();
    this.onClose();
  }
}
