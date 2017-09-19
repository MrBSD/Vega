import { MakesService } from '../../Services/makes.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes:any[];
  constructor(private makesService:MakesService) { }

  ngOnInit() {
    this.makesService.getMakes().subscribe(makes=>this.makes=makes);
    console.log(this.makes);
    
  }

}
