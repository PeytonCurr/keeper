import{u as y,o as I,k as C,P as h,A as a,v as b,c as n,V as B,B as K,_ as S,b as c,d as e,t as l,F as v,i as f,j as r,l as x,p as k,m as w,n as g}from"./index.14535879.js";const A={setup(){const t=y();I(()=>{d(),_()});async function d(){try{await C.getProfileKeeps(t.params.profileId)}catch(s){h.error(s)}}async function _(){try{a.activeVault=null,await b.getProfileVaults(t.params.profileId)}catch(s){h.error(s)}}return{route:t,keeps:n(()=>a.keeps),account:n(()=>a.account),vaults:n(()=>a.vaults),activeVault:n(()=>a.vaults.find(s=>s.creatorId==t.params.profileId))}},components:{VaultCard:B,BasicKeepCard:K}};const i=t=>(k("data-v-b51ac19e"),t=t(),w(),t),F={class:"row px-md-5 pt-4 px-1"},N={class:"col-12 px-md-5 px-1"},j={class:"px-md-5 mx-md-5 mx-1 row profileArea"},D={class:"col-12 text-center"},E=["src"],L={class:"col-12 text-center movePic"},M=["src"],R={class:"mt-2"},q={class:"px-md-5 mx-md-5 mx-1"},z=i(()=>e("h1",null,"Vaults",-1)),G={class:"masonry"},H={class:"px-md-5 mx-md-5 mx-1"},J=i(()=>e("h1",null,"Keeps",-1)),O={class:"masonry"},Q=i(()=>e("br",null,null,-1));function T(t,d,_,s,U,W){var u,p,m;const V=g("VaultCard"),P=g("BasicKeepCard");return r(),c("section",F,[e("div",N,[e("section",j,[e("div",D,[e("img",{class:"coverImg shadow",src:(u=s.activeVault)==null?void 0:u.creator.coverImg,alt:"Profile CoverImg"},null,8,E)]),e("div",L,[e("img",{class:"rounded-circle elevation-5 border border-2",src:(p=s.activeVault)==null?void 0:p.creator.picture,height:"100",alt:"Profile Pic"},null,8,M),e("h1",R,l((m=s.activeVault)==null?void 0:m.creator.name),1),e("h6",null,l(s.vaults.length)+" Vaults | "+l(s.keeps.length)+" Keeps",1)])]),e("div",q,[z,e("section",G,[(r(!0),c(v,null,f(s.vaults,o=>(r(),c("div",{class:"mason elevation-5 rounded",key:o==null?void 0:o.id},[x(V,{vault:o},null,8,["vault"])]))),128))])]),e("div",H,[J,e("section",O,[(r(!0),c(v,null,f(s.keeps,o=>(r(),c("div",{class:"mason elevation-5 rounded",key:o==null?void 0:o.id},[x(P,{keep:o},null,8,["keep"])]))),128))])]),Q])])}const Y=S(A,[["render",T],["__scopeId","data-v-b51ac19e"]]);export{Y as default};
