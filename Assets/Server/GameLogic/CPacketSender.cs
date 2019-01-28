using UnityEngine;
using System.Collections;
using FreeNet;
using LineCrushGameServer;
using System;
//샌더
public static class CPacketSender {

    public static CNetworkManager network_manager { get; set; }

    public static bool isNetManager()
    {
        return network_manager != null;
    }
    public static void DisConnect()
    {
        if (isNetManager())
            network_manager.Disconnect();
    }
    public static void REG_REQ_SEND(string Name, int High, int Mid, int Low)
    {
        //CPacket msg = CPacket.create((short)PROTOCOL.REG_REQ);
        //msg.push(Name);
        //msg.push(High);
        //msg.push(Mid);
        //msg.push(Low);
        //network_manager.send(msg);
    }

    public static string CheckList(string msg)
    {
        msg = msg.Replace("강간", "");
        msg = msg.Replace("살인", "");
        msg = msg.Replace("장기매매", "");
        msg = msg.Replace("매춘", "");
        msg = msg.Replace("창녀", "");
        msg = msg.Replace("오피", "");
        msg = msg.Replace("오피녀", "");
        msg = msg.Replace("룸사롱", "");
        msg = msg.Replace("풀싸롱", "");
        msg = msg.Replace("풀사롱", "");
        msg = msg.Replace("소라넷", "");
        msg = msg.Replace("스와핑", "");
        msg = msg.Replace("토렌트", "");
        msg = msg.Replace("민주화", "");
        msg = msg.Replace("조센징", "");
        msg = msg.Replace("춍", "");
        msg = msg.Replace("바카라", "");
        msg = msg.Replace("사다리", "");
        msg = msg.Replace("장애", "");
        msg = msg.Replace("간통", "");
        msg = msg.Replace("꽃뱀", "");
        msg = msg.Replace("홍어", "");
        msg = msg.Replace("마약", "");
        msg = msg.Replace("농약", "");
        msg = msg.Replace("대포차", "");
        msg = msg.Replace("대포통장", "");
        msg = msg.Replace("대포폰", "");
        msg = msg.Replace("망가", "");
        msg = msg.Replace("민증위조", "");
        msg = msg.Replace("스너프", "");
        msg = msg.Replace("엑스터시", "");
        msg = msg.Replace("헤로인", "");
        msg = msg.Replace("히로뽕", "");
        msg = msg.Replace("suicide", "");
        msg = msg.Replace("zassal", "");
        msg = msg.Replace("대딸", "");
        msg = msg.Replace("립방", "");
        msg = msg.Replace("유리방", "");
        msg = msg.Replace("사행성", "");
        msg = msg.Replace("주민등록증", "");
        msg = msg.Replace("주민등록번", "");
        msg = msg.Replace("민증번호", "");
        msg = msg.Replace("주민번호", "");
        msg = msg.Replace("민증", "");
        msg = msg.Replace("p2p", "");
        msg = msg.Replace("조건녀", "");
        msg = msg.Replace("조건만남", "");
        msg = msg.Replace("현피", "");
        msg = msg.Replace("010", "");
        msg = msg.Replace("070", "");
        msg = msg.Replace("룸망주", "");
        msg = msg.Replace("룸녀", "");
        msg = msg.Replace("벙개", "");
        msg = msg.Replace("만남", "");
        msg = msg.Replace("화대", "");
        msg = msg.Replace("화간", "");
        msg = msg.Replace("떡값", "");
        msg = msg.Replace("시발", "");
        msg = msg.Replace("씨발", "");
        msg = msg.Replace("좆", "");
        msg = msg.Replace("좆나", "");
        msg = msg.Replace("존나", "");
        msg = msg.Replace("창녀", "");
        msg = msg.Replace("샹년", "");
        msg = msg.Replace("썅년", "");
        msg = msg.Replace("샹놈", "");
        msg = msg.Replace("썅놈", "");
        msg = msg.Replace("개새끼", "");
        msg = msg.Replace("씹새끼", "");
        msg = msg.Replace("좆같은새끼", "");
        msg = msg.Replace("시발년", "");
        msg = msg.Replace("시발놈", "");
        msg = msg.Replace("씨발년", "");
        msg = msg.Replace("씨발놈", "");
        msg = msg.Replace("좆년", "");
        msg = msg.Replace("좆놈", "");
        msg = msg.Replace("좆새끼", "");
        msg = msg.Replace("니애미", "");
        msg = msg.Replace("앰창", "");
        msg = msg.Replace("애미창년", "");
        msg = msg.Replace("애비", "");
        msg = msg.Replace("니애비", "");
        msg = msg.Replace("애비창놈", "");
        msg = msg.Replace("느금마", "");
        msg = msg.Replace("느검마", "");
        msg = msg.Replace("느그엄마", "");
        msg = msg.Replace("니엄마", "");
        msg = msg.Replace("네엄마", "");
        msg = msg.Replace("육시랄", "");
        msg = msg.Replace("좆같은년", "");
        msg = msg.Replace("시부랄", "");
        msg = msg.Replace("시부럴", "");
        msg = msg.Replace("씨부랄", "");
        msg = msg.Replace("씨부럴", "");
        msg = msg.Replace("sibal", "");
        msg = msg.Replace("cibal", "");
        msg = msg.Replace("개년", "");
        msg = msg.Replace("시팔", "");
        msg = msg.Replace("씨팔", "");
        msg = msg.Replace("씨벌", "");
        msg = msg.Replace("시펄", "");
        msg = msg.Replace("씨펄", "");
        msg = msg.Replace("쓰벌년", "");
        msg = msg.Replace("쓰부럴년", "");
        msg = msg.Replace("싀발", "");
        msg = msg.Replace("씌발", "");
        msg = msg.Replace("자지", "");
        msg = msg.Replace("보지", "");
        msg = msg.Replace("좆", "");
        msg = msg.Replace("씹", "");
        msg = msg.Replace("불알", "");
        msg = msg.Replace("젖", "");
        msg = msg.Replace("섹스", "");
        msg = msg.Replace("쎅스", "");
        msg = msg.Replace("섹", "");
        msg = msg.Replace("쎅", "");
        msg = msg.Replace("입싸", "");
        msg = msg.Replace("입사", "");
        msg = msg.Replace("질싸", "");
        msg = msg.Replace("질사", "");
        msg = msg.Replace("귀두", "");
        msg = msg.Replace("사까시", "");
        msg = msg.Replace("자위", "");
        msg = msg.Replace("씹질", "");
        msg = msg.Replace("후장", "");
        msg = msg.Replace("짬지", "");
        msg = msg.Replace("잠지", "");
        msg = msg.Replace("씹보지", "");
        msg = msg.Replace("씹자지", "");
        msg = msg.Replace("오랄", "");
        msg = msg.Replace("오럴", "");
        msg = msg.Replace("창녀", "");
        msg = msg.Replace("blowjob", "");
        msg = msg.Replace("oral", "");
        msg = msg.Replace("sex", "");
        msg = msg.Replace("boji", "");
        msg = msg.Replace("jaji", "");
        msg = msg.Replace("jot", "");
        msg = msg.Replace("ero", "");
        msg = msg.Replace("porno", "");
        msg = msg.Replace("씹물", "");
        msg = msg.Replace("블로우잡", "");
        msg = msg.Replace("파이즈리", "");
        msg = msg.Replace("질내사정", "");
        msg = msg.Replace("보짓물", "");
        msg = msg.Replace("보지물", "");
        msg = msg.Replace("정액", "");
        msg = msg.Replace("망가", "");
        msg = msg.Replace("포르노", "");
        msg = msg.Replace("에로", "");
        msg = msg.Replace("섹스코리아", "");
        msg = msg.Replace("소라넷", "");
        msg = msg.Replace("강간", "");
        msg = msg.Replace("야동", "");
        msg = msg.Replace("창년", "");
        msg = msg.Replace("페티쉬", "");
        msg = msg.Replace("누드", "");
        msg = msg.Replace("딸딸이", "");
        msg = msg.Replace("떡치기", "");
        msg = msg.Replace("떢치기", "");
        msg = msg.Replace("돌림빵", "");
        msg = msg.Replace("뽀르노", "");
        msg = msg.Replace("성기", "");
        msg = msg.Replace("뒷치기", "");
        msg = msg.Replace("유두", "");
        msg = msg.Replace("전립선", "");
        msg = msg.Replace("로리타", "");
        msg = msg.Replace("미아리", "");
        msg = msg.Replace("빠구리", "");
        msg = msg.Replace("색스", "");
        msg = msg.Replace("야설", "");
        msg = msg.Replace("야한동영상", "");
        msg = msg.Replace("야사", "");
        msg = msg.Replace("성폭행", "");
        msg = msg.Replace("색마", "");
        msg = msg.Replace("섹마", "");
        msg = msg.Replace("체위", "");
        msg = msg.Replace("성인용품", "");
        msg = msg.Replace("클리토리스", "");
        msg = msg.Replace("자궁", "");
        msg = msg.Replace("자살", "");
        msg = msg.Replace("개세끼", "");
        msg = msg.Replace("오피", "");
        msg = msg.Replace("뒤치기", "");
        msg = msg.Replace("sex", "");
        msg = msg.Replace("Fuck", "Fxxk");
        return msg;
    }


    public static void CREATE_ROOM_REQ_SEND(string _msg, int maxUser)
    {
        _msg = CheckList(_msg);
        CPacket msg = CPacket.create((short)PROTOCOL.CREATE_ROOM_REQ);
        msg.push(_msg);
        msg.push(maxUser);
        network_manager.send(msg);
    }

    public static void ROOM_LIST_REQ_SEND()
    {
        CPacket msg = CPacket.create((short)PROTOCOL.ROOM_LIST_REQ);
        network_manager.send(msg);
    }
    public static void ROOM_EXIT_REQ_SEND()
    {
        CPacket msg = CPacket.create((short)PROTOCOL.ROOM_EXIT_REQ);
        network_manager.send(msg);
    }

    public static void CHAT_MSG_REQ_SEND(string _msg)
    {
        CPacket msg = CPacket.create((short)PROTOCOL.CHAT_MSG_REQ);
        msg.push(_msg);
        network_manager.send(msg);
    }
    public static void ROOM_CONNECT_REQ_SEND(int _roomNum)
    {
        CPacket msg = CPacket.create((short)PROTOCOL.ROOM_CONNECT_REQ);
        msg.push(_roomNum);
        network_manager.send(msg);
    }
    public static void MOVING_USER_REQ_SEND(int SN,Vector3 pos)
    {
        CPacket msg = CPacket.create((short)PROTOCOL.MOVING_USER_REQ);
        msg.push(SN);
        msg.push(pos.x);
        msg.push(pos.y);
        msg.push(pos.z);
        network_manager.send(msg);
    }

    public static void ID_CHANGE_REQ_SEND(string name, int level)
    {
        CPacket msg = CPacket.create((short)PROTOCOL.ID_CHANGE_REQ);
        msg.push(name);
        msg.push(level);
        network_manager.send(msg);
    }

    public static void HEART_SEND_REQ_SEND(int SN)
    {
        CPacket msg = CPacket.create((short)PROTOCOL.HEART_SEND_REQ);
        msg.push(SN);
        network_manager.send(msg);
    }
}
