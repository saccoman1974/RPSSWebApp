import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [mode, setMode] = useState('classic');
    const [choices, setChoices] = useState([]);
    const [selectedChoice, setSelectedChoice] = useState('');
    const [userChoice, setUserChoice] = useState('');
    const [computerChoice, setComputerChoice] = useState('');
    const [result, setResult] = useState('');
    const [loading, setLoading] = useState(false);
    const [outcomeReason, setOutcomeReason] = useState('');

    useEffect(() => {
        const endpoint = mode === 'Enhanced' ? '/api/game/enchancedchoices' : '/api/game/choices';
        fetch(endpoint)
            .then(res => res.json())
            .then(setChoices);
    }, [mode]);

    async function play() {
        if (!selectedChoice) return;
        setLoading(true);
        setUserChoice('');
        setComputerChoice('');
        setResult('');
        const response = await fetch('/api/game/play', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ UserChoice: selectedChoice, Mode: mode.charAt(0).toUpperCase() + mode.slice(1) })
        });
        if (response.ok) {
            const data = await response.json();
            setUserChoice(data.userChoice);
            setComputerChoice(data.computerChoice);
            setResult(data.result);
            setOutcomeReason(data.outcomeReason || '');
        }
        setLoading(false);
    }

    return (
        <div className="game-container">
            <h1>Rock Paper Scissors Game</h1>
            <div className="mode-select">
                <label>
                    <input type="radio" value="Classic" checked={mode === 'Classic'} onChange={() => setMode('Classic')} />
                    Classic
                </label>
                <label>
                    <input type="radio" value="Enhanced" checked={mode === 'Enhanced'} onChange={() => setMode('Enhanced')} />
                    Extended
                </label>
            </div>
            <div className="instruction">
                <p>Please select one of the choices below and press Play:</p>
            </div>
            <div className="choices">
                {choices.map(choice => (
                    <label key={choice} style={{ marginRight: '1em' }}>
                        <input
                            type="radio"
                            name="user-choice"
                            value={choice}
                            checked={selectedChoice === choice}
                            onChange={() => setSelectedChoice(choice)}
                            disabled={loading}
                        />
                        {choice}
                    </label>
                ))}
            </div>
            <button onClick={play} disabled={loading || !selectedChoice} style={{ marginTop: '1em' }}>Play</button>
            {result && (
                <div className="result" style={{ marginTop: '2em', textAlign: 'center' }}>
                    <div style={{ fontWeight: 'bold', fontSize: '2em', marginBottom: '1em' }}>You {result}</div>
                    <div style={{ fontSize: '1.2em' }}>
                        You chose: <b>{userChoice}</b><br />
                        Computer chose: <b>{computerChoice}</b>
                        <br />
                        <br />
                        <br />
                        <b>{outcomeReason} </b>
                    </div>
                </div>
            )}
        </div>
    );
}

export default App;