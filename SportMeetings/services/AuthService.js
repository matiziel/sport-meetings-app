import ApiClient from "./ApiClient.js";
import Config from "../config/Config.js";
import * as SecureStore from 'expo-secure-store';
import Token from "../Token.js";


const AuthService = {
    getAuthHeader: async () => {
        const user = JSON.parse(await SecureStore.getItemAsync(Config.UserAuthDataKey));
        return {
            headers: { Authorization: 'Bearer ' + user.accessToken }
        }
    },
    signIn: async (username, password) => {
        const response = await ApiClient.post(
            'authentication/login',
            {
                Username: username,
                Password: password
            });
        await SecureStore.deleteItemAsync(Config.UserAuthDataKey);
        await SecureStore.setItemAsync(Config.UserAuthDataKey, JSON.stringify(response.data));
    },
    signOut: async () => {
        await SecureStore.deleteItemAsync(Config.UserAuthDataKey);
    },
    signUp: async (username, email, password) => {
        await ApiClient.post(
            'authentication/register',
            {
                Username: username,
                Email: email,
                Password: password
            });
    },
    refreshToken: async () => {
        const user = await SecureStore.getItemAsync(Config.UserAuthDataKey);
        const response = await ApiClient.post(
            'authentication/refreshToken',
            {
                AccessToken: user.accessToken,
                RefreshToken: user.refreshToken
            });
        await SecureStore.setItemAsync(Config.UserAuthDataKey, JSON.stringify(response.data));
    }
};

export default AuthService;