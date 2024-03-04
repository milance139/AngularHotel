import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
} from '@angular/core';
import { ICurrency } from '../../shared/models/Currency';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule, formatDate } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CurrencyService } from '../../../services/currency.service';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-currency-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './currency-form.component.html',
  styleUrl: './currency-form.component.scss',
})
export class CurrencyFormComponent implements OnChanges {
  @Input() data: ICurrency | null = null;
  @Output() onCloseModal = new EventEmitter();

  currencyForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private currencyService: CurrencyService,
    private toastr: ToastrService
  ) {
    this.currencyForm = this.fb.group({
      id: [''],
      name: new FormControl('', [Validators.required]),
      price: new FormControl('', [Validators.required]),
    });
  }

  onClose() {
    this.onCloseModal.emit(false);
  }

  ngOnChanges(): void {
    if (this.data) {
      this.currencyForm.patchValue({
        id: this.data.id,
        name: this.data.name,
        price: this.data.price
      });
    }
  }

  onSubmit() {
    if (this.currencyForm.valid) {
      if (this.data) {
        this.currencyService
          .updateCurrency(this.currencyForm.value)
          .subscribe({
            next: (response: any) => {
              this.resetCurrencyForm();
              this.toastr.success(response.message);
            },
          });
      } else {
        delete this.currencyForm.value.id;
        this.currencyService.createCurrency(this.currencyForm.value).subscribe({
          next: (response: any) => {
            this.resetCurrencyForm();
            this.toastr.success(response.message);
          },
        });
      }
    } else {
      this.currencyForm.markAllAsTouched();
    }
  }

  resetCurrencyForm() {
    this.currencyForm.reset();
    this.onClose();
  }
}
