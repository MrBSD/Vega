import { FeaturesService } from '../../Services/features.service';
import { MakesService } from '../../Services/makes.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  makes:any[];
  models: any[];
  vehicle:any={};
  features:any[];

  constructor(
    private makesService:MakesService,
    private feturesService: FeaturesService) { }

  ngOnInit() {
    this.makesService.getMakes().subscribe(makes=>this.makes=makes);
    this.feturesService.getFeatures().subscribe(features=>this.features=features);
          
  }

  onMakeChange(){
    var selectedMake= this.makes.find(m=>m.id==this.vehicle.make);
    this.models = selectedMake ? selectedMake.models:[];
  }

}
