import { useState, useContext, useEffect } from 'react';
import { Button, Text, TextInput, View, TouchableWithoutFeedback } from 'react-native';
import AuthContext from '../../context/AuthContext.js';
import Styles from '../../styles/Styles.js';
import SportEventsService from '../../services/SportEventsService.js';
import SignUpsService from '../../services/SignUpsService.js';
import { FlatList } from 'react-native-gesture-handler';
import Moment from 'moment';

function SportEventInfoScreen({ route, navigation }) {
    const [sportEvent, setSportEvent] = useState({ id: null });
    const [signedUp, setSignedUp] = useState(false);
    const [isEventOwner, setIsEventOwner] = useState(false);

    Moment.locale('pl');


    useEffect(() => {
        const fetchData = async () => {
            const { sportEventId } = route.params;
            const result = await SportEventsService.getSportEvent(sportEventId);
            setSportEvent(result);

            const isSignedUp = await SignUpsService.isUserSignedUpForEvent(sportEventId);
            setSignedUp(isSignedUp);

            const isOwner = await SportEventsService.isUserEventOwner(sportEventId);
            setIsEventOwner(isOwner);
        };
        fetchData();
    }, [sportEvent.id, signedUp]);

    const onSignUp = async (eventId) => {
        await SignUpsService.signUpForEvent(eventId);
        setSignedUp(true);
    }

    const onSignOut = async (eventId) => {
        await SignUpsService.signOutFromEvent(eventId);
        setSignedUp(false);
    }

    return (
        <View style={Styles.eventInfoStyle}>
            {isEventOwner && <Button title="Edit event" onPress={async () => navigation.navigate('UpdateSportEvent', { sportEventId: sportEvent.id })}> </Button>}
            {sportEvent && <Text style={Styles.eventInfoTextStyle}>Name: {sportEvent.name}</Text>}
            {sportEvent && <Text style={Styles.eventInfoTextStyle}>Description: {sportEvent.description}</Text>}
            {sportEvent && <Text style={Styles.eventInfoTextStyle}>Number of free spaces: {sportEvent.numberOfFreeSpaces} / {sportEvent.limitOfParticipants}</Text>}
            {sportEvent && <Text style={Styles.eventInfoTextStyle}>Start date: {Moment(sportEvent.startDate).format("DD.MM.YYYY hh:mm")}</Text>}
            {sportEvent && <Text style={Styles.eventInfoTextStyle}>Duration: {sportEvent.durationInHours} h</Text>}
            {sportEvent && <Text style={Styles.eventInfoTextStyle}>Location: {sportEvent.location}</Text>}
            {(!signedUp && sportEvent.numberOfFreeSpaces > 0) && <Button title="Sign up for event" onPress={async () => await onSignUp(sportEvent.id)}> </Button>}
            {signedUp && <Button title="Sign out from event" onPress={async () => await onSignOut(sportEvent.id)}> </Button>}
        </View>);
}

export default SportEventInfoScreen;