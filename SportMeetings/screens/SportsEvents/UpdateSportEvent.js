import { useState, useContext, useEffect } from 'react';
import { Button, Text, TextInput, View } from 'react-native';
import AuthContext from '../../context/AuthContext.js';
import Styles from '../../styles/Styles.js';

function UpdateSportEventScreen({ navigation }) {
    const [name, setName] = React.useState('');
    const [description, setDescription] = React.useState('');
    const [limitOfParticipants, setLimitOfParticipants] = React.useState('');
    const [startDate, setStartDate] = React.useState('');
    const [durationInHours, setDurationInHours] = React.useState('');
    const [location, setLocation] = React.useState('');

    const createSportEvent = async (sportEvent) => {
        await SignUpsService.signUpForEvent(eventId);
        setSignedUp(true);
    }

    return (
        <View style={Styles.container}>

        </View>
    );
}

export default UpdateSportEventScreen;
