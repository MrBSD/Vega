import { Contact } from './Contact';
import { KeyValuePair } from './KeyValuePair';


export interface SaveVehicle {
        id: number;
        modelId: number;
        makeId: number;
        isRegistered: boolean;
        features: number[];
        contact: Contact;
}