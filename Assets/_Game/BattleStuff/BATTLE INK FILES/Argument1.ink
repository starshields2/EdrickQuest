VAR YMorale = 0 
VAR JMorale = 0 
VAR EdrickPoints = 100


->Yael 

==Yael

+[Please, just use the tonic on her.] 
{YMorale < 3: NO. It's not FOR her.}
{YMorale >= 3: ...fine. I suppose this is the time to break the rules.}

*{EdrickPoints > 1} [Don't you think there's a reason Jasper hasn't washed her cloak?]
You can't be insinuating that there's a REASON she hasn't washed it. 
~increaseYMorale(4)
~decreaseEdrickPoints(1)
**[Well, she is a Dawnkeeper. Their cloaks are special - I hear they can only wash them at a certain time of day.]
Well now I feel like a total jerk. Why didn't you tell me that?
***[You didn't ask.]
You- whatever!
**[Have you asked her?]
...no.
~increaseYMorale(3)
~decreaseEdrickPoints(1)
->Yael

*{EdrickPoints >1} [I'm sure you don't smell like roses either, Yael.   ]
~decreaseEdrickPoints(1)
~decreaseYMorale(1)
I certainly smell better than she does.
**[I highly doubt that, but as a ghost, I can't confirm.]
-Continue backseat driving Edrick, that's fine by me. 
->DONE

==function increaseYMorale(amount)
~YMorale = YMorale + amount

==function decreaseEdrickPoints(amount)
~EdrickPoints = EdrickPoints - amount

==function decreaseYMorale(amount)
~YMorale = YMorale -1