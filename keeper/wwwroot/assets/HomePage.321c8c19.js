import{_,o as i,w as u,k as l,P as n,c as m,K as f,C as v,q as y,s as g,b as t,F as K,i as C,j as s,l as h,A as c,v as k,n as w}from"./index.f0974cb0.js";const x={setup(){i(()=>{r()}),u(()=>{var e;(e=c.account)!=null&&e.id&&o()});async function o(){try{await k.getMyVaults()}catch(e){n.error(e)}}async function r(){try{await l.getKeeps()}catch(e){n.error(e)}}return{keeps:m(()=>c.keeps)}},components:{KeepCard:f,CreateKeep:v,KeepDetails:y,CreateVault:g}},P={class:"p-md-5 p-1 masonry"};function V(o,r,e,p,b,M){const d=w("KeepCard");return s(),t("section",P,[(s(!0),t(K,null,C(p.keeps,a=>(s(),t("div",{class:"mason elevation-5 rounded",key:a==null?void 0:a.id},[h(d,{keep:a},null,8,["keep"])]))),128))])}const $=_(x,[["render",V],["__scopeId","data-v-0857a1db"]]);export{$ as default};
