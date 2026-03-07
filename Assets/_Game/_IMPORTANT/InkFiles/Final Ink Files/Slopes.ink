//InkOnly
VAR YaelTell = 0
VAR jaspersWay = false
VAR yaelsWay = false
VAR YaelExhaust = 0
VAR JasperExhaust = 0

//External Variables
VAR ExternalTutorialNum = 0
VAR PopupNum = 0
VAR tension = 14
VAR NotesIndex = 0
VAR success = false
//VAR failure = false
VAR passive = false
VAR type = 2
// ^^^ 0 = None, 1 = Mediation, 2 = Event, 3 = Cutscene

#YaelPensive
Ah, stop, stop. This creature is in pain.
+[Continue. #EdrickContinue]

#Jasper
So unfortunate... I'll put it out of its misery. Turn around, Yael, don't look.
++[Continue. #EdrickContinue]
#YaelAngry
No! I can heal it. I regard all life with dignity, and I can prolong this one!
+++[Contine #EdrickContinue]
#Jasper
And waste precious time? Don't be silly.
++++[Let me sort this out. #EdrickContinue]  ->MEDIATIONSTART

=MEDIATIONSTART
#Edrick
What should we do?
+[Yael should heal it. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[Jasper should kill it. #EdrickChoice]
~SetJaspersWay()
->JASPER
+[You two decide. #EdrickChoice] ->EDRICK
->DONE

=YAEL
#Yael
{yaelsWay: 
{tension > 13: You've made no mistake in listening to my wisdom! #Yael}
{tension <= 12: Let's see if this will work. #Yael}
}
+[Let's do this. #EdrickContinue]
~CalculateEventResults()
->YAELCOMP
->DONE

=JASPER
#Jasper
{jaspersWay: 
{tension > 13: Good thing you went with my idea, Calibrator! #Jasper}
{tension <= 12: I hope it'll work, thanks for believing in me! #Jasper }
}

+[Okay, let's try this. #EdrickContinue]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END
=EDRICK
->DONE

=JASPERCOMP
{passive: You did nothing.}
{ success:
I've killed it, don't worry. It won't be in pain anymore. #Jasper
- else:
I couldn't do it. #Jasper
}
->DONE
=YAELCOMP

{passive: You did nothing.}


{ success:
#Yael
I'll sit here with it and rock it to its final resting place.
- else:
I wasn't able to heal it. It's passed on.
}
->DONE


==function IncreaseTension(amount)
~tension = tension + amount

==function DecreaseTension(amount)
~tension = tension - amount

==function SetJaspersWay
~jaspersWay = true

==function SetYaelsWay
~yaelsWay = true

==function ChangeNotesIndex(amount)
~NotesIndex = amount

==function ExtendedTutorial(amount)
~ExternalTutorialNum = amount

==function UpdatePopupNumber(amount)
~PopupNum = amount

==function IncreaseYaelExhaust(amount)
~YaelExhaust = YaelExhaust + amount

EXTERNAL UpdateNote()
EXTERNAL ShowObjection()
EXTERNAL CalculateEventResults()