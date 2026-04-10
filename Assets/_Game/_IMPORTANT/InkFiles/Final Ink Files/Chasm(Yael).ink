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

#Jasper
Great. We're literally in between a rock and a hard place.
+[Continue. #EdrickContinue]
~IncreaseTension(2)
#YaelAngry
And whose fault is that?
++[Continue. #EdrickContinue]
#Jasper
...Yours?
+++[Contine #EdrickContinue]
~SetSuccessTrue()
->DONE
=MEDIATIONSTART
#Edrick
What should we do?
+[We should use magic. #EdrickChoice]
~SetYaelsWay()
->YAEL
+[We should use the tree. #EdrickChoice]
~SetJaspersWay()
->JASPER
~CalculateEventResults()
->EDRICK
->DONE

=YAEL
#Yael
{yaelsWay: 
{tension > 13: You've made no mistake in listening to my wisdom!}
{tension <= 12: Let's see if this will work. }
}
#Yael
+[Let's do this. #EdrickContinue]
~CalculateEventResults()
->YAELCOMP
->DONE

=JASPER
{jaspersWay: 
{tension > 13: Good thing you went with my idea, Calibrator! }
{tension <= 12: I hope it'll work, thanks for believing in me! }
}
#Jasper
+[Okay, let's try this. #EdrickContinue]
~CalculateEventResults()
->JASPERCOMP
->DONE

->END
=EDRICK
{success: We are going around the bridge. No arguments.| We will have to find another way forward. }
->DONE

=JASPERCOMP
{passive: You did nothing.}
{ success:
I've cut the tree down. We can progress from here with no issue.
- else:
Blast! I've just made the chasm bigger. 
}
->DONE
=YAELCOMP

{passive: You did nothing.}


{ success:
See? Now we can step across, with no issue.
- else:
Ah, I see the problem. We're still not able to cross...
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

==function SetSuccessTrue()
~success = true

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