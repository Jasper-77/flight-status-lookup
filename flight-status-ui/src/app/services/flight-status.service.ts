import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FlightStatusResult } from '../models/flight-status-result';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FlightStatusService {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7035/flights/status';

  getFlightStatus(
    flightNumber: string,
    date: string
  ): Observable<FlightStatusResult> {

    return this.http.get<FlightStatusResult>(
      `${this.apiUrl}?flightNumber=${flightNumber}&date=${date}`
    );
  }
}