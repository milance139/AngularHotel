import { Component, OnInit } from '@angular/core';
import { ModalComponent } from '../../ui/modal/modal.component';
import { CurrencyFormComponent } from '../currency-form/currency-form.component';
import { ToastrService } from 'ngx-toastr';
import { CurrencyService } from '../../../services/currency.service';
import { ICurrency } from '../../shared/models/Currency';

@Component({
  selector: 'app-currency',
  standalone: true,
  imports: [ModalComponent, CurrencyFormComponent],
  templateUrl: './currency.component.html',
  styleUrl: './currency.component.scss',
})
export class CurrencyComponent implements OnInit {
  isModalOpen = false;
  currencies: ICurrency[] = [];
  currency!: ICurrency;

  constructor(
    private currencyService: CurrencyService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getAllCurrencies();
  }

  getAllCurrencies() {
    this.currencyService.getAllCurrencies().subscribe({
      next: (response) => {
        if (response.data) {
          this.currencies = response.data;
        }
      }
    });
  }

  loadCurrency(currency: ICurrency) {
    this.currency = currency;
    this.openModal();
  }

  deleteCurrency(id: number) {
    this.currencyService.deleteCurrency(id).subscribe({
      next: (response) => {
      console.log("hello")
        this.toastr.success(response.message);
        this.getAllCurrencies();
      },
    });
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.getAllCurrencies();
  }
}
