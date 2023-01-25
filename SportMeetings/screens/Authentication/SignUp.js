import * as React from 'react';
import {
    View,
    TextInput,
    Button,

} from "react-native";

import AuthContext from '../../context/AuthContext.js';
import Styles from '../../styles/Styles.js';


function SignUpScreen({ navigation }) {
    const [username, setUsername] = React.useState('');
    const [email, setEmail] = React.useState('');
    const [password, setPassword] = React.useState('');

    const { signUp } = React.useContext(AuthContext);


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
                    placeholder="Email"
                    value={email}
                    onChangeText={setEmail}
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
            <Button title="Sign up" onPress={() => signUp({ username, email, password })} />
            <Button title="Sign in" onPress={() => navigation.navigate('SingIn')} />
        </View>
    );
}




export default SignUpScreen;