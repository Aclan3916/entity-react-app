import { useState, useEffect } from 'react'
import PersonList from './components/PersonList.tsx';
import PersonForm from "./components/PersonForm.tsx";

import './App.css'
import type { Person } from "./types/Person.ts";

function App() {
    const[people, setPeople] = useState<Person[]>([]);
    const[isLoading, setIsLoading] = useState(false);
    
    //GET 
    useEffect(() => {
        const fetchPeople = async () => {
            try {
                const res = await fetch('http://localhost:5094/api/people');
                const data = await res.json();
                setPeople(data);
            } catch (err) {
                console.error('Error fetching people:', err)
            } finally {
                setIsLoading(false);
            }
        }
         fetchPeople();
    }, []);
    const addPerson = async (person : Omit<Person, 'id'>) =>
    {
        const res = await fetch('http://localhost:5094/api/people',
            {
                method: 'POST',
                headers: {'Content-Type' : 'application/json'},
                body : JSON.stringify(person)
            })
        if(res.ok)
        {
            const newPerson = await res.json()
            setPeople((prev) => [...prev, newPerson])
        }
    }
    
    const deletePerson = async (id: number) =>
    {
        const res = await fetch(`http://localhost:5094/api/people/${id}`,
            {
                method: 'DELETE'
            }
        )
            if(res.ok)
            {
                setPeople((prev) => prev.filter((x) => x.id !== id))
            }
    }
    
    if(isLoading) return <p>Loading...</p>
    
  return (
    <>
        <h1>Contact Manager</h1>
        <PersonForm onAdd={addPerson} />
        
        <PersonList people={people} onDelete={deletePerson} />
    </>
  )
}

export default App
