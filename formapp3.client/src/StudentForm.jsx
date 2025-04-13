//StudentForm.jsx
import React, { useState, useEffect } from 'react';
import './StudentForm.css';

function StudentForm() {
    const [nume, setNume] = useState('')
    const [prenume, setPrenume] = useState('')
    const [facultate, setFacultate] = useState('')
    const [motivare, setMotivare] = useState('')
    const [isSubmitting, setIsSubmitting] = useState(false);


    //functie anonima ?
    const handleSubmit=(e) => {
        e.preventDefault(); // no reloading page
        console.log({nume, prenume, facultate, motivare})
        fetch('https://localhost:7145/api/StudentForms', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({nume,prenume, facultate, motivare})
        })
            .then(response => {
                console.log('Response status:', response.status);
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                console.log('Success:', data);
                generatePdf();
            })
            .catch(error => {
                console.error('Error:', error);
                // Handle error (show message to user)
            });
    }

    const generatePdf = () => {
        fetch('https://localhost:7145/api/Pdf/generate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ nume, prenume, facultate, motivare })
        })
            .then(response => {
                console.log('Response status:', response.status);
                if (!response.ok) {
                    throw new Error(`HTTP error! Status: ${response.status}`);
                }
                return response.blob(); 
            })
            .then(blob => { //blob = binary large object
                const url = window.URL.createObjectURL(blob); // Create a URL for the blob
                const a = document.createElement('a'); // Create an anchor element
                a.href = url;
                a.download = 'StudentForm_' + nume + '_' + prenume + '.pdf'; // file name

                document.body.appendChild(a); // Append to body
                a.click(); // download

                window.URL.revokeObjectURL(url);
                document.body.removeChild(a);
                setIsSubmitting(true); // submitted state = true
            })
            .catch(error => {
                console.error('Error:', error);
                setIsSubmitting(false);
            });
    }

    useEffect(() => {
        fetch('https://localhost:7267/api/StudentForms')
            .then(response => response.json())
            .then(data => console.log(data))
            .catch(error => console.log('Error:', error));
    }, [])

    return (
        <div>
            <h2>Formular</h2>

            <form onSubmit={handleSubmit}>
                <div>
                    <label> Nume: </label>
                    <input name="inputNume" type="text" onChange={(e) => setNume(e.target.value)} required />
                </div>
                <div>
                    <label> Prenume: </label>
                    <input name="inputPrenume" type="text" onChange={(e) => setPrenume(e.target.value)} required />
                </div>
                <div>
                    <label> Facultate: </label>
                    <input name="inputFacultate" type="text" onChange={(e) => setFacultate(e.target.value)} required />
                </div>
                <div>
                    <label> Motivare: </label>
                    <input name="inputMotivare" type="text" onChange={(e) => setMotivare(e.target.value)} required />
                </div>
                <button type="submit" disabled={isSubmitting}>{isSubmitting ? 'Se trimite..' : 'Trimite'}</button>
            </form>
        </div>
    );
}

export default StudentForm;