var downcount = require('./js/downcount.js');
var prompt = require('prompt');
var async = require("async");

console.log('Welcome to Downcount');

PlayGame();


///
/// Play Downcount
///
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
    prompt.get(schema, function (err, result) {
        if (err) { return onErr(err); }
        
        // Validate the input
        var targetNumber = parseInt(result.targetNumber);
        if (isNaN(targetNumber)) {
            return onErr('Invalid target number entered');
        }

        var integers = IntegerArrayFromString(result.integers);
        
        console.log('The game is to find ' + targetNumber + ' from the set of numbers ' + 
            integers + ', you have 30 seconds starting now');
        console.log('');
        
        // This should run async
        var solver = new downcount.DownCountSolver();
        var solutions = solver.Solve(targetNumber, integers, true, false);
        
        var tick = 30;
        var interval = setInterval(function () {
            process.stdout.clearLine();  // clear current text
            process.stdout.cursorTo(0);
            process.stdout.write(tick.toString());
            --tick;
            if (tick == -1) {
                clearInterval(interval);
                console.log('');
                ShowResults(solutions, targetNumber);
                
            }
        }, 1000);        
    });
}

///
/// Analyse the solutions returned by downcount and display to the user
///
function ShowResults(solutions, targetNumber) {
    console.log('');    
    if (solutions.Equations.length > 0) {
        if (solutions.Equations[0].Value() == targetNumber) {
            console.log('Downcount found an exact solution');
            console.log(solutions.Equations[0].toString() + ' = ' + solutions.Equations[0].Value());
        }
        else {
            var foundValue = solutions.Equations[0].Value();
            console.log('Downcount couldn\'t find an exact solution but found a solution within ' + (targetNumber - foundValue) + ' of ' + targetNumber);
            console.log(solutions.Equations[0].toString() + ' = ' + foundValue);
        }
    }
    else {
        console.log('Downcount couldn\'t find an exact or close solution');
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

///
/// Return an array from a comman seperated list of integers
///
function IntegerArrayFromString(numbers) {
    if (typeof numbers !== 'string') {
        throw "IntegerSet: numbers not a string";
    }
    
    var integerSet = [];
    var res = numbers.split(",");
    for (var i = 0; i < res.length; ++i) {
        var number = parseInt(res[i]);
        if (isNaN(number)) {
            throw "Invalid number creating IntegerSet";
        }
        integerSet.push(number);
    }

    return integerSet;
}

///
/// Handle errors
///
function onErr(err) {
    console.log('Downcount encountered an error: ' + err);
    return 1;
}




