import * as React from 'react';
import {
    View,
    TextInput,
    Button,

} from "react-native";

import AuthContext from '../../context/AuthContext.js';
import Styles from '../../styles/Styles.js';



function SignInScreen({ navigation }) {
    const [username, setUsername] = React.useState('');
    const [password, setPassword] = React.useState('');

    const { signIn } = React.useContext(AuthContext);


    return (
        <View style={Styles.container}>
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.TextInput}
                    placeholder="Username"
                    value={username}
                    onChangeText={setUsername}
                />
            </View>
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.TextInput}
                    placeholder="Password"
                    value={password}
                    onChangeText={setPassword}
                    secureTextEntry={true}
                />
            </View>

            <Button title="Sign in" onPress={() => signIn({ username, password })} />
            <Button title="Sign up" onPress={() => navigation.navigate('SignUp')} />

        </View>
    );
}




export default SignInScreen;
