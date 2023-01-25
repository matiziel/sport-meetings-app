import { useState, useContext, useEffect } from 'react';
import { Button, Text, TextInput, View } from 'react-native';
import AuthContext from '../../context/AuthContext.js'
import TestService from '../../services/TestService.js';

function UserScreen() {
    const { signOut } = useContext(AuthContext);

    useEffect(() => {
        const fetchData = async () => {
            const result = await TestService.getTestValue();
            setValue(result);
        };
        fetchData();
    });

    return (
        <View>
            <Button title="Sign out" onPress={signOut} />

            
        </View>
    );
}

export default UserScreen;