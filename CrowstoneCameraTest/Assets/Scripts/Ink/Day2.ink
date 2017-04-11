CONST Currency = 0
VAR day_Complete = false
VAR wanted_Poster = false
VAR showed_Poster_Kathrine = false
VAR met_Pemberton = false
VAR met_Morgen = false
VAR morgan_Ran = false




===Pemberton===

{met_Pemberton:->second_meeting |->first_meeting}

=first_meeting
Good morning, Sheriff. I'm glad to see you're an early riser. I have a new lead for you to follow. There's a man in town, Jacob Morgan. I believe he's caught wind of my presence. ->converstation_options
=converstation_options
*[Have any tips on how I should handle him?]
    He seems a hard man, so I would suggest that in addition to trying to persuade him, you could try the route of blackmail. I'll bet he has some dirt on him. He seems the type. Of course, you're always welcome to try the other options we discussed yesterday. -> converstation_options
+[I'd best get to work, then.]
    ~met_Pemberton = true
    ->DONE
    
=second_meeting
I trust you were successful again, Sheriff?
+[Not yet]
    ->DONE
*{morgan_Ran}[Yes, Jacob won't be a problem anymore.]
    Another job well done. You're proving to be quite the ally, Sheriff. I'm going to do a little more detective work on another of your citizens. Come back tomorrow and I should have more information for you.
    **[Right. Until then.]
        ~day_Complete = true
        ->DONE


===Jacob_Morgen===
{met_Morgen: ->second_meeting|->first_meeting}
=first_meeting
What the hell do you want, Sheriff? Can't a man walk the streets without a lawman riding his coat tails.
* [I hear you've been poking your nose into business that isn't yours, Jacob.] The man in the red hat...I need you to stop talking about him.
        And why should I listen to you? Who cares if it's my business or not? It's a free country, last I checked. I can talk about anybody I want to.
        **[I'll be back to continue this conversation later.]
            ~met_Morgen =true
            ->DONE
=second_meeting
Back again, Sheriff? I thought I made it clear that I didn't want to talk to you.
*{wanted_Poster} [Jacob, I found this. Care to explain how your face got on this wanted poster?]
    What?! Now, Sheriff...let's not be hasty here. I'm not lookin' to hang today....
    **[Then you'd better leave town, Jacob. I don't want to see you around here for a while.]
        ~morgan_Ran =true
        Right, Sheriff. I got some loose ends to tie up today, then I'll get outta your hair. I swear.
        ->DONE
+[I'll be back Morgen]
    ->DONE

===wanted_Poster===
    ~wanted_Poster = true
    ->DONE
            
===Harriet===
Well, good day to you, Sheriff. What can I do you for?
+[Nothing for now. have a good day Miss.]
    ->DONE
+[What can you tell me about Jacob Morgan?]
    Jacob Morgan? Oh, yes! He's the new man in town, right? The rugged fellow. I know a little bit, Sheriff. What would you like to know about him? I'll see if I can help. ->investigate
=investigate
    
*[What is he like?]
        Hard to tell, really. He's a firm man, and he rarely smiles. I've noticed he looks an awful lot like one of the wanted posters you have hanging up on your wall. 'Black Jack?' But surely that can't be him.... Is there anything else you need to know?->investigate
*[Do you know who he associates with in town?]
        Between you, me, and the tree, Sheriff, I've seen him over at Katherine Blakley's home. I think he fancies her. Isn't young love sweet? What else can I do you for, Sheriff?->investigate
+[Nothing for now. have a good day Miss.]
    ->DONE
    
    
===Katherine_Blakely===
Good day, Sheriff! You don't make your way out this way too often. What can I do for you?

*{wanted_Poster}[I understand you've been seeing Jacob Morgan]
    Wh-what is this...? A wanted poster? With Jacob on it? This can't be...I thought I knew him. I need to speak to him right now. Thank you, Sheriff. ->DONE
    
+[Nothing for now Ms. Blakley]
    ->DONE
    

