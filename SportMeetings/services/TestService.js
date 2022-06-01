import ApiClient from "./ApiClient.js";
import AuthService from "./AuthService.js";


const TestService = {
    getTestValue: async () => {
        try {
            var result = await ApiClient.get(
                'authentication/test',
                {
                    headers: await AuthService.getAuthHeader()
                });

            return result.data;
        }
        catch (error) {
            console.log(error);
        }
    }
};

export default TestService;