import { SaveVehicle } from '../Models/SaveVehicle';
import { Vehicle } from './../Models/Vehicle';
import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import 'rxjs/add/operator/map';

@Injectable()
export class VehicleService {
    private readonly vehicleEndpoint = '/api/vehicles';

    constructor(private http: Http) {
        
     }
 
     
    create(vehicle) {
        return this.http.post(this.vehicleEndpoint, vehicle)
            .map(res => res.json());
    }

    delete(id){
        return this.http.delete(this.vehicleEndpoint+ '/' + id)
            .map(res => res.json());
    }
    
    getMakes() {
        return this.http.get('/api/makes')
            .map(res => res.json());
    }

    getFeatures() {
        return this.http.get('/api/features')
            .map(res => res.json());

    }

    getVehicle(id) {
        return this.http.get(this.vehicleEndpoint +'/' + id)
            .map(res => res.json());
    }

    getVehicles(filter){
        return this.http.get(this.vehicleEndpoint+'?'+ this.toQueryString(filter))
        .map(res => res.json());
    }

    toQueryString(obj){
        var parts: string[] = [];
        for (var property in obj)
        {
            var value=obj[property];
            if (value !=null && value!=undefined)
                parts.push(encodeURIComponent(property)+'='+encodeURIComponent(value));
        }

        return parts.join('&');
    }

    update (vehicle: SaveVehicle){
        return this.http.put(this.vehicleEndpoint +'/' + vehicle.id, vehicle)
        .map(res => res.json());
    }
}