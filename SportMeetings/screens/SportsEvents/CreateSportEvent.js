import { Button, TextInput, View } from 'react-native';
import Styles from '../../styles/Styles.js';
import DatePicker from 'react-native-date-picker';
import { useState, useContext, useEffect } from 'react';

function CreateSportEventScreen({ navigation }) {
    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [limitOfParticipants, setLimitOfParticipants] = useState('');
    const [startDate, setStartDate] = useState(new Date());
    const [durationInHours, setDurationInHours] = useState('');
    const [location, setLocation] = useState('');

    const createSportEvent = async (sportEvent) => {
        console.log(sportEvent);
    }

    return (
        <View style={Styles.container}>
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.TextInput}
                    placeholder="Name"
                    value={name}
                    onChangeText={setName}
                />
            </View>
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.TextInput}
                    placeholder="Description"
                    value={description}
                    onChangeText={setDescription}
                />
            </View>
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.inputView}
                    placeholder="Limit of participants"
                    keyboardType='numeric'
                    onChangeText={setLimitOfParticipants}
                    value={limitOfParticipants}
                    maxLength={3}
                />
            </View>
            <DatePicker date={startDate} onDateChange={setStartDate} />
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.inputView}
                    placeholder="Duration in hours"
                    keyboardType='numeric'
                    onChangeText={setDurationInHours}
                    value={durationInHours}
                    maxLength={1}
                />
            </View>
            <View style={Styles.inputView}>
                <TextInput
                    style={Styles.TextInput}
                    placeholder="Location"
                    value={location}
                    onChangeText={setLocation}
                />
            </View>

            <Button title="Create" onPress={() =>
                createSportEvent({
                    name, description, limitOfParticipants, startDate, durationInHours, location
                })
            } />

        </View>
    );
}

export default CreateSportEventScreen;
