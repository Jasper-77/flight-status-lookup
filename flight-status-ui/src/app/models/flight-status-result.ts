export interface FlightStatusResult {
  airline?: string;
  flightNumber: string;
  departureAirport?: string;
  arrivalAirport?: string;
  scheduledDepartureUtc: string;
  actualDepartureUtc?: string;
  scheduledArrivalUtc: string;
  actualArrivalUtc?: string;
  status: string;
  gate?: string;
  terminal?: string;
  delayReason?: string;
  provider: string;
  message?: string;
}