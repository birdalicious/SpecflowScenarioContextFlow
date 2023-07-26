export interface StepRegex {
    [key: string] : {
        scenarioBlock: "Step" | "Given" | "When" | "Then";
        stepDefinition: string;
    }
}

export class StepMatcher {
    stepRegex: StepRegex;
    private AnyBlockRegex = "Given|When|Then|And";

    constructor(stepRegex: StepRegex) {
        this.stepRegex = stepRegex;
    }

    public match(step: string) {
        return new Promise((resolve, reject) => {
            let match: string[] | null = null;
            
            Object.keys(this.stepRegex).some(regex => {
                match = step.match(new RegExp(`^(${this.AnyBlockRegex}) ${regex}\$`));
                return match
            });

            resolve(match);
        });
    }
}