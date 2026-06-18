import { Component, signal } from '@angular/core';
import { FlightStatus } from './components/flight-status/flight-status';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FlightStatus],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected readonly title = signal('flight-status-ui');
}