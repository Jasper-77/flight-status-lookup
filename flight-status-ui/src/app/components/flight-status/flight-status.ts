import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { FlightStatusService } from '../../services/flight-status.service';
import { FlightStatusResult } from '../../models/flight-status-result';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@Component({
  selector: 'app-flight-status',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './flight-status.html',
  styleUrl: './flight-status.scss'
})
export class FlightStatus {

  private fb = inject(FormBuilder);

  private service = inject(FlightStatusService);
  private cdr = inject(ChangeDetectorRef);
  result?: FlightStatusResult;

  errorMessage = '';

  loading = false;

  form = this.fb.group({
    flightNumber: ['', Validators.required],
    date: ['', Validators.required]
  });

  search(): void {
    if (this.form.invalid) {
      return;
    }

    const value = this.form.getRawValue();

    const selectedDate = new Date(value.date!);

    const formattedDate = selectedDate.toISOString().split('T')[0];

    this.loading = true;
    this.errorMessage = '';

    this.service.getFlightStatus(
      value.flightNumber!,
      formattedDate
    )
    .subscribe({
      next: response => {
        console.log('API Response', response);
        this.result = { ...response };
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.loading = false;
        this.errorMessage =  'Unable to retrieve flight information.';
        this.cdr.detectChanges();
      }
    });
  }

  getStatusClass(status: string): string {

    switch (status) {

      case 'OnTime':
        return 'green';

      case 'Delayed':
        return 'amber';

      case 'Cancelled':
      case 'Diverted':
        return 'red';

      default:
        return 'grey';
    }
  }
}