import type { Person } from '../types/Person.ts'

type PersonListProps = {
    people: Person[],
    onDelete: (id: number) => void
}
const PersonList = ({people, onDelete} : PersonListProps) =>
{
    
    return(
        <>
            <h2> People</h2>
            {people.map((person) =>
                <li key={person.id}>{person.name} - {person.email} - {person.phone}
                <button onClick={() => onDelete(person.id)}>Delete</button></li>)}
        </>
    )
}   

export default PersonList;