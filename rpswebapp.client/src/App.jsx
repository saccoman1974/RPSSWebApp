import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [mode, setMode] = useState('classic');
    const [choices, setChoices] = useState([]);
    const [userChoice, setUserChoice] = useState('');
    const [computerChoice, setComputerChoice] = useState('');
    const [result, setResult] = useState('');
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        fetch(`/api/game/choices?mode=${mode}`)
            .then(res => res.json())
            .then(setChoices);
    }, [mode]);

    async function play(choice) {
        setLoading(true);
        setUserChoice('');
        setComputerChoice('');
        setResult('');
        const response = await fetch('/api/game/play', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ userChoice: choice, mode })
        });
        if (response.ok) {
            const data = await response.json();
            setUserChoice(data.userChoice);
            setComputerChoice(data.computerChoice);
            setResult(data.result);
        }
        setLoading(false);
    }

    return (
        <div className="game-container">
            <h1>Rock Paper Scissors{mode === 'enhanced' && ' Lizard Spock'}</h1>
            <div className="mode-select">
                <label>
                    <input type="radio" value="classic" checked={mode === 'classic'} onChange={() => setMode('classic')} />
                    Classic
                </label>
                <label>
                    <input type="radio" value="enhanced" checked={mode === 'enhanced'} onChange={() => setMode('enhanced')} />
                    Extended
                </label>
            </div>
            <div className="choices">
                {choices.map(choice => (
                    <button key={choice} onClick={() => play(choice)} disabled={loading}>{choice}</button>
                ))}
            </div>
            {result && (
                <div className="result">
                    <p>You chose: <b>{userChoice}</b></p>
                    <p>Computer chose: <b>{computerChoice}</b></p>
                    <h2>Result: {result}</h2>
                </div>
            )}
        </div>
    );
}

export default App;