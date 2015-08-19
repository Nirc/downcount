var downcount = require('./js/downcount.js');
var prompt = require('prompt');

console.log('Welcome to Downcount');

PlayGame();


function PlayGame() {
    console.log('');

    // Get the game input from the player
    var schema = {
        properties: {
            integers: {
                message: 'Enter a comma seperated list of integers, e.g. 3,5,67,8 and press enter',
                required: true
            },
            targetNumber: {
                message: 'Enter your target number and press enter',
                required: true
            }
        }
    };
    
    prompt.start();
    var integers;
    var targetNumber;
    prompt.get(schema, function (err, result) {
        if (err) { return onErr(err); }
        
        // Validate the input
        targetNumber = parseInt(result.targetNumber);
        if (isNaN(targetNumber)) {
            return onErr('Invalid target number entered')
        }

        integers = result.integers;
        
        console.log('The game is to find ' + targetNumber + ' from the set of numbers ' + integers 
            + ', you have 30 seconds starting now');
        console.log('');
        
        // This should run async
        var solver = new downcount.DownCountSolver();
        var solutions = solver.Solve(targetNumber, integers, false);
        
        var tick = 30;
        var interval = setInterval(function () {
            process.stdout.clearLine();  // clear current text
            process.stdout.cursorTo(0);
            process.stdout.write(tick.toString());
            --tick;
            if (tick == -1) {
                clearInterval(interval);
                console.log('');
                console.log('');
                if (solutions.Equations.length > 0) {
                    console.log('Downcount found an exact solution');
                    console.log(solutions.Equations[0].toString() + ' = ' + solutions.Equations[0].Value());
                }
                else {
                    console.log('Downcount couldn\'t find an exact solution');
                }
                
                // Shall we play again
                console.log('');
                console.log('Do you wish to play again, Y/N');
                prompt.start();
                prompt.get(['playAgain'], function (err, result) {
                    if (err) { return onErr(err); }
                    if (result.playAgain == 'y' || result.playAgain == 'Y') {
                        PlayGame();
                    }
                });
            }
        }, 1000);        
    });
}

function onErr(err) {
    console.log('Downcount encountered an error: ' + err);
    return 1;
}




