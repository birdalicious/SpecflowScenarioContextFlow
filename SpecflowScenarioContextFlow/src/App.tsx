import { useState } from 'react'
import './App.css'

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCaretDown } from '@fortawesome/free-solid-svg-icons'

const tempString = `Given user adds an estimate
When user submits an estimate
Then user rejects an estimate`

function App() {
  const [steps, setSteps] = useState<string[]>(tempString.split("\n"))

  return (
    <>
      <textarea readOnly value={tempString}/>
      <div id="flowContainer">
        <Step type="Given" name="user adds an estimate"/>
        <Step type="When" name="user submits an estimate"/>
        <Step type="Then" name="user rejects an estimate"/>
      </div>
    </>
  )
}

interface StepProps {
  type: "Given" | "When" | "Then" | "And";
  name: string;
}
function Step({type, name} : StepProps) {
  return <div className="step"> 
    <div className="stepTitle">
      <div className="stepType">{type}</div>
      <div className="stepName">{name}</div>
      <div className="stepDropDownButton"><FontAwesomeIcon icon={faCaretDown} /></div>
    </div>
    <div className="stepContext">
      <div className="stepSets">
        <div className="stepContextTitle">Sets</div>
        <div>Estimate.ContainerId</div>
      </div>
      <div className="stepGets">
        <div className="stepContextTitle">Gets</div>
        <div>Estimate.ContainerId</div>
      </div>
    </div>
  </div> 
}

export default App
