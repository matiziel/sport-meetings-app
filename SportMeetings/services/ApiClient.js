import axios from "axios";
import Config from "../config/Config.js";

const ApiClient = axios.create({
    baseURL: Config.ApiUrl
});

export default ApiClient;