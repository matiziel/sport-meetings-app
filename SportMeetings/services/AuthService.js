import ApiClient from "./ApiClient.js";
import Config from "../config/Config.js";
import * as SecureStore from 'expo-secure-store';
import Token from "../Token.js";


const AuthService = {
    getAuthHeader: async () => {
        const user = JSON.parse(await SecureStore.getItemAsync(Config.UserAuthDataKey));
        return { Authorization: 'Bearer ' + user.accessToken };
    },
    signIn: async (username, password) => {
        const response = await ApiClient.post(
            'authentication/login',
            {
                Username: username,
                Password: password
            });
        await SecureStore.setItemAsync(Config.UserAuthDataKey, JSON.stringify(response.data));
        // Token.accessToken = response.data.accessToken;
    },
    signOut: async () => {
        await SecureStore.deleteItemAsync(Config.UserAuthDataKey);
        // Token.accessToken = null;
    },
    singUp: async (username, email, password) => {
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
        // Token.accessToken = response.data.accessToken;
    }
};

export default AuthService;