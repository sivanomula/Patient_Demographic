import { Component, OnInit } from '@angular/core';
import { PatientService } from '../patient.service';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {
  patients: Array<any>;
  selectedPatient: any;
  constructor(private service: PatientService) {
    this.getPatients();


  }
  getPatients() {
    this.service.getPatients().subscribe(p => {
      this.patients = p as Array<any>;
      this.patients.forEach(p => {
        p.dateofBirth = this.formatDate(p.dateofBirth);
      });
    });
  }
  ngOnInit() {
  }
  onSelect(patient: any): void {
    this.selectedPatient = patient;
  }
  AddNew() {
    this.selectedPatient =
      {

        "forenames": "",
        "surname": "",
        "dateofBirth": "",
        "Gender":"M",
        "telephonenumbers": [
          {
            "phoneType": 0,
            "number": "",
            "type": "Home"
          },
          {
            "phoneType": 2,
            "number": "",
            "type": "Mobile"
          },
          {
            "phoneType": 1,
            "number": "",
            "type": "Work"
          }
        ]
      }
    this.patients.push(this.selectedPatient);

  }
  formatDate(date) {
    var d = new Date(date),
      month = '' + (d.getMonth() + 1),
      day = '' + d.getDate(),
      year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
  }
}
