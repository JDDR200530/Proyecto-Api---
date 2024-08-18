
import axios from"axios";
const API_URL = 'https://localhost:7179/api';

const PackageService = axios.create({
    baseURL: API_URL,
    headers: {
        "Content-Type": "application/json"
    },
    
});

export {
    PackageService,
    API_URL
}