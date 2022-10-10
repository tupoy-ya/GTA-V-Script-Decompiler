﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decompiler.Enums
{
    internal enum EventType
    {
        EVENT_ACQUAINTANCE_PED_DISLIKE,
        EVENT_ACQUAINTANCE_PED_HATE,
        EVENT_ACQUAINTANCE_PED_LIKE,
        EVENT_ACQUAINTANCE_PED_RESPECT,
        EVENT_ACQUAINTANCE_PED_WANTED,
        EVENT_ACQUAINTANCE_PED_DEAD,
        EVENT_AGITATED,
        EVENT_AGITATED_ACTION,
        EVENT_ENCROACHING_PED,
        EVENT_CALL_FOR_COVER,
        EVENT_CAR_UNDRIVEABLE,
        EVENT_CLIMB_LADDER_ON_ROUTE,
        EVENT_CLIMB_NAVMESH_ON_ROUTE,
        EVENT_COMBAT_TAUNT,
        EVENT_COMMUNICATE_EVENT,
        EVENT_COP_CAR_BEING_STOLEN,
        EVENT_CRIME_REPORTED,
        EVENT_DAMAGE,
        EVENT_DEAD_PED_FOUND,
        EVENT_DEATH,
        EVENT_DRAGGED_OUT_CAR,
        EVENT_DUMMY_CONVERSION,
        EVENT_EXPLOSION,
        EVENT_EXPLOSION_HEARD,
        EVENT_FIRE_NEARBY,
        EVENT_FLUSH_TASKS,
        EVENT_FOOT_STEP_HEARD,
        EVENT_GET_OUT_OF_WATER,
        EVENT_GIVE_PED_TASK,
        EVENT_GUN_AIMED_AT,
        EVENT_HELP_AMBIENT_FRIEND,
        EVENT_INJURED_CRY_FOR_HELP,
        EVENT_CRIME_CRY_FOR_HELP,
        EVENT_IN_AIR,
        EVENT_IN_WATER,
        EVENT_INCAPACITATED,
        EVENT_LEADER_ENTERED_CAR_AS_DRIVER,
        EVENT_LEADER_ENTERED_COVER,
        EVENT_LEADER_EXITED_CAR_AS_DRIVER,
        EVENT_LEADER_HOLSTERED_WEAPON,
        EVENT_LEADER_LEFT_COVER,
        EVENT_LEADER_UNHOLSTERED_WEAPON,
        EVENT_MELEE_ACTION,
        EVENT_MUST_LEAVE_BOAT,
        EVENT_NEW_TASK,
        EVENT_NONE,
        EVENT_OBJECT_COLLISION,
        EVENT_ON_FIRE,
        EVENT_OPEN_DOOR,
        EVENT_SHOVE_PED,
        EVENT_PED_COLLISION_WITH_PED,
        EVENT_PED_COLLISION_WITH_PLAYER,
        EVENT_PED_ENTERED_MY_VEHICLE,
        EVENT_PED_JACKING_MY_VEHICLE,
        EVENT_PED_ON_CAR_ROOF,
        EVENT_PED_TO_CHASE,
        EVENT_PED_TO_FLEE,
        EVENT_PLAYER_COLLISION_WITH_PED,
        EVENT_PLAYER_LOCK_ON_TARGET,
        EVENT_POTENTIAL_BE_WALKED_INTO,
        EVENT_POTENTIAL_BLAST,
        EVENT_POTENTIAL_GET_RUN_OVER,
        EVENT_POTENTIAL_WALK_INTO_FIRE,
        EVENT_POTENTIAL_WALK_INTO_OBJECT,
        EVENT_POTENTIAL_WALK_INTO_VEHICLE,
        EVENT_PROVIDING_COVER,
        EVENT_RADIO_TARGET_POSITION,
        EVENT_RAN_OVER_PED,
        EVENT_REACTION_COMBAT_VICTORY,
        EVENT_REACTION_ENEMY_PED,
        EVENT_REACTION_INVESTIGATE_DEAD_PED,
        EVENT_REACTION_INVESTIGATE_THREAT,
        EVENT_REQUEST_HELP_WITH_CONFRONTATION,
        EVENT_RESPONDED_TO_THREAT,
        EVENT_REVIVED,
        EVENT_SCRIPT_COMMAND,
        EVENT_SHOCKING_BROKEN_GLASS,
        EVENT_SHOCKING_CAR_ALARM,
        EVENT_SHOCKING_CAR_CHASE,
        EVENT_SHOCKING_CAR_CRASH,
        EVENT_SHOCKING_BICYCLE_CRASH,
        EVENT_SHOCKING_CAR_PILE_UP,
        EVENT_SHOCKING_CAR_ON_CAR,
        EVENT_SHOCKING_DANGEROUS_ANIMAL,
        EVENT_SHOCKING_DEAD_BODY,
        EVENT_SHOCKING_DRIVING_ON_PAVEMENT,
        EVENT_SHOCKING_BICYCLE_ON_PAVEMENT,
        EVENT_SHOCKING_ENGINE_REVVED,
        EVENT_SHOCKING_EXPLOSION,
        EVENT_SHOCKING_FIRE,
        EVENT_SHOCKING_GUN_FIGHT,
        EVENT_SHOCKING_GUNSHOT_FIRED,
        EVENT_SHOCKING_HELICOPTER_OVERHEAD,
        EVENT_SHOCKING_PARACHUTER_OVERHEAD,
        EVENT_SHOCKING_PED_KNOCKED_INTO_BY_PLAYER,
        EVENT_SHOCKING_HORN_SOUNDED,
        EVENT_SHOCKING_IN_DANGEROUS_VEHICLE,
        EVENT_SHOCKING_INJURED_PED,
        EVENT_SHOCKING_MAD_DRIVER,
        EVENT_SHOCKING_MAD_DRIVER_EXTREME,
        EVENT_SHOCKING_MAD_DRIVER_BICYCLE,
        EVENT_SHOCKING_MUGGING,
        EVENT_SHOCKING_NON_VIOLENT_WEAPON_AIMED_AT,
        EVENT_SHOCKING_PED_RUN_OVER,
        EVENT_SHOCKING_PED_SHOT,
        EVENT_SHOCKING_PLANE_FLY_BY,
        EVENT_SHOCKING_POTENTIAL_BLAST,
        EVENT_SHOCKING_PROPERTY_DAMAGE,
        EVENT_SHOCKING_RUNNING_PED,
        EVENT_SHOCKING_RUNNING_STAMPEDE,
        EVENT_SHOCKING_SEEN_CAR_STOLEN,
        EVENT_SHOCKING_SEEN_CONFRONTATION,
        EVENT_SHOCKING_SEEN_GANG_FIGHT,
        EVENT_SHOCKING_SEEN_INSULT,
        EVENT_SHOCKING_SEEN_MELEE_ACTION,
        EVENT_SHOCKING_SEEN_NICE_CAR,
        EVENT_SHOCKING_SEEN_PED_KILLED,
        EVENT_SHOCKING_SEEN_VEHICLE_TOWED,
        EVENT_SHOCKING_SEEN_WEAPON_THREAT,
        EVENT_SHOCKING_SEEN_WEIRD_PED,
        EVENT_SHOCKING_SEEN_WEIRD_PED_APPROACHING,
        EVENT_SHOCKING_SIREN,
        EVENT_SHOCKING_STUDIO_BOMB,
        EVENT_SHOCKING_VISIBLE_WEAPON,
        EVENT_SHOT_FIRED,
        EVENT_SHOT_FIRED_BULLET_IMPACT,
        EVENT_SHOT_FIRED_WHIZZED_BY,
        EVENT_FRIENDLY_AIMED_AT,
        EVENT_FRIENDLY_FIRE_NEAR_MISS,
        EVENT_SHOUT_BLOCKING_LOS,
        EVENT_SHOUT_TARGET_POSITION,
        EVENT_STATIC_COUNT_REACHED_MAX,
        EVENT_STUCK_IN_AIR,
        EVENT_SUSPICIOUS_ACTIVITY,
        EVENT_SWITCH_2_NM_TASK,
        EVENT_UNIDENTIFIED_PED,
        EVENT_VEHICLE_COLLISION,
        EVENT_VEHICLE_DAMAGE_WEAPON,
        EVENT_VEHICLE_ON_FIRE,
        EVENT_WHISTLING_HEARD,
        EVENT_DISTURBANCE,
        EVENT_ENTITY_DAMAGED,
        EVENT_ENTITY_DESTROYED,
        EVENT_WRITHE,
        EVENT_HURT_TRANSITION,
        EVENT_PLAYER_UNABLE_TO_ENTER_VEHICLE,
        EVENT_SCENARIO_FORCE_ACTION,
        EVENT_STAT_VALUE_CHANGED,
        EVENT_PLAYER_DEATH,
        EVENT_PED_SEEN_DEAD_PED,
        EVENT_0xC92B98C8,
        EVENT_NETWORK_PLAYER_JOIN_SESSION,
        EVENT_NETWORK_PLAYER_LEFT_SESSION,
        EVENT_NETWORK_PLAYER_JOIN_SCRIPT,
        EVENT_NETWORK_PLAYER_LEFT_SCRIPT,
        EVENT_NETWORK_STORE_PLAYER_LEFT,
        EVENT_NETWORK_SESSION_START,
        EVENT_NETWORK_SESSION_END,
        EVENT_NETWORK_START_MATCH,
        EVENT_NETWORK_END_MATCH,
        EVENT_NETWORK_REMOVED_FROM_SESSION_DUE_TO_STALL,
        EVENT_NETWORK_REMOVED_FROM_SESSION_DUE_TO_COMPLAINTS,
        EVENT_NETWORK_CONNECTION_TIMEOUT,
        EVENT_NETWORK_PED_DROPPED_WEAPON,
        EVENT_NETWORK_PLAYER_SPAWN,
        EVENT_NETWORK_PLAYER_COLLECTED_PICKUP,
        EVENT_NETWORK_PLAYER_COLLECTED_AMBIENT_PICKUP,
        EVENT_NETWORK_PLAYER_COLLECTED_PORTABLE_PICKUP,
        EVENT_NETWORK_PLAYER_DROPPED_PORTABLE_PICKUP,
        EVENT_NETWORK_INVITE_ARRIVED,
        EVENT_NETWORK_INVITE_ACCEPTED,
        EVENT_NETWORK_INVITE_CONFIRMED,
        EVENT_NETWORK_INVITE_REJECTED,
        EVENT_NETWORK_SUMMON,
        EVENT_NETWORK_SCRIPT_EVENT,
        EVENT_NETWORK_PLAYER_SIGNED_OFFLINE,
        EVENT_NETWORK_SIGN_IN_STATE_CHANGED,
        EVENT_NETWORK_NETWORK_ROS_CHANGED,
        EVENT_NETWORK_SIGN_IN_CHANGE_ACTIONED,
        EVENT_NETWORK_NETWORK_BAIL,
        EVENT_NETWORK_HOST_MIGRATION,
        EVENT_NETWORK_FIND_SESSION,
        EVENT_NETWORK_HOST_SESSION,
        EVENT_NETWORK_JOIN_SESSION,
        EVENT_NETWORK_JOIN_SESSION_RESPONSE,
        EVENT_NETWORK_CHEAT_TRIGGERED,
        EVENT_NETWORK_DAMAGE_ENTITY,
        EVENT_NETWORK_PLAYER_ARREST,
        EVENT_NETWORK_TIMED_EXPLOSION,
        EVENT_0x272B4BB1,
        EVENT_0x03C0D0BB,
        EVENT_0xC5857F28,
        EVENT_0x6B39C3C7,
        EVENT_VOICE_SESSION_STARTED,
        EVENT_VOICE_SESSION_ENDED,
        EVENT_VOICE_CONNECTION_REQUESTED,
        EVENT_VOICE_CONNECTION_RESPONSE,
        EVENT_VOICE_CONNECTION_TERMINATED,
        EVENT_TEXT_MESSAGE_RECEIVED,
        EVENT_CLOUD_FILE_RESPONSE,
        EVENT_NETWORK_PICKUP_RESPAWNED,
        EVENT_NETWORK_PRESENCE_STAT_UPDATE,
        EVENT_NETWORK_PED_LEFT_BEHIND,
        EVENT_NETWORK_INBOX_MSGS_RCVD,
        EVENT_NETWORK_ATTEMPT_HOST_MIGRATION,
        EVENT_NETWORK_INCREMENT_STAT,
        EVENT_NETWORK_SESSION_EVENT,
        EVENT_NETWORK_TRANSITION_STARTED,
        EVENT_NETWORK_TRANSITION_EVENT,
        EVENT_NETWORK_TRANSITION_MEMBER_JOINED,
        EVENT_NETWORK_TRANSITION_MEMBER_LEFT,
        EVENT_NETWORK_TRANSITION_PARAMETER_CHANGED,
        EVENT_NETWORK_CLAN_KICKED,
        EVENT_NETWORK_TRANSITION_STRING_CHANGED,
        EVENT_NETWORK_TRANSITION_GAMER_INSTRUCTION,
        EVENT_NETWORK_PRESENCE_INVITE,
        EVENT_NETWORK_PRESENCE_INVITE_REMOVED,
        EVENT_NETWORK_PRESENCE_INVITE_REPLY,
        EVENT_NETWORK_CASH_TRANSACTION_LOG,
        EVENT_NETWORK_CLAN_RANK_CHANGE,
        EVENT_NETWORK_VEHICLE_UNDRIVABLE,
        EVENT_NETWORK_PRESENCE_TRIGGER,
        EVENT_NETWORK_PRESENCE_EMAIL,
        EVENT_NETWORK_FOLLOW_INVITE_RECEIVED,
        EVENT_NETWORK_ADMIN_INVITED,
        EVENT_NETWORK_SPECTATE_LOCAL,
        EVENT_NETWORK_CLOUD_EVENT,
        EVENT_NETWORK_SHOP_TRANSACTION,
        EVENT_NETWORK_PERMISSION_CHECK_RESULT,
        EVENT_NETWORK_APP_LAUNCHED,
        EVENT_NETWORK_ONLINE_PERMISSIONS_UPDATED,
        EVENT_NETWORK_SYSTEM_SERVICE_EVENT,
        EVENT_NETWORK_REQUEST_DELAY,
        EVENT_NETWORK_SOCIAL_CLUB_ACCOUNT_LINKED,
        EVENT_NETWORK_SCADMIN_PLAYER_UPDATED,
        EVENT_NETWORK_SCADMIN_RECEIVED_CASH,
        EVENT_NETWORK_CLAN_INVITE_REQUEST_RECEIVED,
        EVENT_0x93AC2785,
        EVENT_NETWORK_STUNT_PERFORMED,
        EVENT_NETWORK_FIRED_DUMMY_PROJECTILE,
        EVENT_NETWORK_PLAYER_ENTERED_VEHICLE,
        EVENT_NETWORK_PLAYER_ACTIVATED_SPECIAL_ABILITY,
        EVENT_NETWORK_PLAYER_DEACTIVATED_SPECIAL_ABILITY,
        EVENT_NETWORK_PLAYER_SPECIAL_ABILITY_FAILED_ACTIVATION,
        EVENT_NETWORK_FIRED_VEHICLE_PROJECTILE,
        EVENT_NETWORK_SC_MEMBERSHIP_STATUS,
        EVENT_NETWORK_SC_BENEFITS_STATUS,
        EVENT_0x4A8A5373,
        EVENT_ERRORS_UNKNOWN_ERROR,
        EVENT_ERRORS_ARRAY_OVERFLOW,
        EVENT_ERRORS_INSTRUCTION_LIMIT,
        EVENT_ERRORS_STACK_OVERFLOW,
        EVENT_0x063E563B,
        EVENT_0x9DEA6A90,
        EVENT_INVALID = -1
    }
}
