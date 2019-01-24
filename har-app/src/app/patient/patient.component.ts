import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient',
  templateUrl: './patient.component.html',
  styleUrls: ['./patient.component.css']
})
export class PatientComponent implements OnInit {
  @Input() patient: any;
  @Output() patientadd = new EventEmitter();
  constructor(private service: PatientService) { }

  ngOnInit() {
  }
  save(): void {
    this.service.addPatient(this.patient)
      .subscribe(() => this.goBack());
  }
  goBack(): void {
    delete this.patient;
    this.patientadd.emit(this.patient);
  }

}
