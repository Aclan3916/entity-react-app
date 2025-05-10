import {useState} from 'react'
import type { Person } from '../types/Person.ts'

type PersonFormProps = {
    onAdd: (person : Omit<Person, 'id'>) => void;
};
const PersonForm = ({onAdd}: PersonFormProps) => {
    const[name, setName] = useState('');
    const[phone, setPhone] = useState('');
    const[email, setEmail] = useState('');
    
    const handleSubmit = async (e : React.FormEvent) =>
    {
        e.preventDefault();
        if(!name.trim() || !email.trim() || !phone.trim()) 
            return;
        
        const newPerson : Omit<Person, 'id'> = {
            name,
            phone,
            email
        }

        // Clear form
        setName('');
        setPhone('');
        setEmail('');
        onAdd(newPerson); //let parent refresh list
       
    };
    
    return (
        <>
            <form onSubmit={handleSubmit}>
                <input placeholder="name" type="text" value ={name} onChange={(e) => setName(e.target.value)} />
                <input placeholder="phone" type="text" value={phone} onChange={(e) => setPhone(e.target.value)} />
                <input placeholder= "email" type="text" value ={email} onChange={(e) => setEmail(e.target.value)} />
                <button type="submit">Add Person</button>
            </form>
        </>
    )
}

export default PersonForm;