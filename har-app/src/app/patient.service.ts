import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PatientService{
  private patientUrl = 'http://localhost:61156/api/patient';
   httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json', 'Access-Control-Allow-Origin':'*' })
  };
  constructor(private http: HttpClient) { }

  getPatients(): Observable<any> {
    return this.http.get<any>(this.patientUrl).pipe(
      tap(_ => this.log('fetched Patients')),
      catchError(this.handleError('getPatients', []))
    );
  }
  getPatient(id: number): Observable<any> {
    const url = `${this.patientUrl}/${id}`;
    return this.http.get<any>(url).pipe(
      tap(_ => this.log(`fetched patient id=${id}`)),
      catchError(this.handleError<any>(`getPatient id=${id}`))
    );
  }
  updatePatient (patient: any): Observable<any> {
    const url = `${this.patientUrl}/${patient.id}`;
    return this.http.put(url, patient, this.httpOptions).pipe(
      tap(_ => this.log(`updated patient id=${patient.id}`)),
      catchError(this.handleError<any>('updatePatient'))
    );
  }
  addPatient (patient: any): Observable<any> {
    return this.http.post<any>(this.patientUrl, patient, this.httpOptions).pipe(
      tap((patient: any) => this.log(`added patient w/ id=${patient.id}`)),
      catchError(this.handleError<any>('addPatient'))
    );
  }
  log(msg: string): void {
    console.log(msg);
  }
  handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      this.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return of(result as T);
    };
  }
  error(error: any): any {
    console.error(error);
  }
}
