import { createStackNavigator } from '@react-navigation/stack';
import { NavigationContainer } from '@react-navigation/native';
import SignInScreen from './SignIn';
import SignUpScreen from './SignUp';



const Stack = createStackNavigator();

function AuthScreen() {
    return (
        <Stack.Navigator initialRouteName="SingIn">
            <Stack.Screen name="SingIn" component={SignInScreen} />
            <Stack.Screen name="SignUp" component={SignUpScreen} />
        </Stack.Navigator>
    );
}

export default AuthScreen;
